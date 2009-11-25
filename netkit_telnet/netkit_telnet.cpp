// netkit_telnet.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#pragma comment(lib, "Ws2_32.lib")

HANDLE connected;
HANDLE closed;
SOCKET sock;
HANDLE in;
HANDLE out;
BOOL exited;
sockaddr addr;

DWORD WINAPI work(LPVOID)
{
	sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	BOOL nodelay = TRUE;
	setsockopt(sock, IPPROTO_TCP, TCP_NODELAY, (char*)&nodelay, 1);

	timeval timeout;
	timeout.tv_sec = 20;
	timeout.tv_usec = 0;

	if (connect(sock, (sockaddr*)&addr, sizeof(addr)) != 0) {
		std::cerr << "Failed to connect: " << GetLastError() << std::endl;
		exited = true;
		SetEvent(connected);
		SetEvent(closed);
		return -1;
	}

	SetEvent(connected);

	while (true)
	{
		char buffer[256];
		int i = recv(sock, buffer, 256, 0);
		if (i < 0)
		{
			std::cerr << "Recv Error: " << GetLastError() << std::endl;
			closesocket(sock);
			break;
		}
		if (i == 0)
		{
			std::cerr << "Server closed connection." << std::endl;
			shutdown(sock, SD_SEND);
			break;
		}
		else
		{
			DWORD j;
			for (int total = 0; total < i; total += j)
			{
				if (!WriteFile(out, buffer + total, i - total, &j, NULL))
				{
					closesocket(sock);
					break;
				}
			}
		}
	}

	SetEvent(closed);
	return 0;
}

int _tmain(int argc, _TCHAR* argv[])
{
	WSADATA wsaData;
	int iResult = WSAStartup(MAKEWORD(2,2), &wsaData);
	if (iResult != NO_ERROR) {
		std::cerr << "Error at WSAStartup()" << std::endl;
		return 1;
	}

	out = GetStdHandle(STD_OUTPUT_HANDLE);
	exited = false;

	struct addrinfoW aiHints;
	struct addrinfoW *aiList = NULL;
	memset(&aiHints, 0, sizeof(aiHints));
	aiHints.ai_family = AF_INET;
	aiHints.ai_socktype = SOCK_STREAM;
	aiHints.ai_protocol = IPPROTO_TCP;

	if (GetAddrInfoW(argv[1], argv[2]==NULL?_T("telnet"):argv[2], &aiHints, &aiList) != 0) {
		printf("GetAddrInfoW() failed.\n");
	}

	addr = *aiList->ai_addr;

	connected = CreateEvent(NULL, FALSE, FALSE, _T("ConnectedEvent"));
	closed = CreateEvent(NULL, FALSE, FALSE, _T("ClosedEvent"));
	CreateThread(NULL, 0, work, NULL, 0, NULL);
	WaitForSingleObject(connected, INFINITE);

	in = GetStdHandle(STD_INPUT_HANDLE);

	while (!exited)
	{
		char buffer[256];
		DWORD i;
		if (!ReadFile(in, buffer, 256, &i, NULL))
		{
			std::cerr << "Closing." << std::endl;
			shutdown(sock, SD_SEND);
			exited = true;
		}
		else
		{
			int j;
			for (unsigned int total = 0; total < i; total += j)
			{
				j = send(sock, buffer + total, i - total, 0);
				if (j <= 0)
				{
					exited = true;
					break;
				}
			}
		}
	}

	WaitForSingleObject(closed, INFINITE);

	WSACleanup();
	return 0;
}
