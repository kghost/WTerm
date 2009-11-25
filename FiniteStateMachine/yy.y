%{
using System.Collections.Generic;
namespace FiniteStateMachine {
%}

%token L R C O S LS RS LC RC E CL DL D

%start  result

%%

result: regexs regex E
		{
			$$ = $2;
		}

regexs:
	| regexs alias

alias: DL string CL regex E
		{
			if (((Dictionary<string, StateMachine>)yyContext).ContainsKey((string)$2))
				throw new Exception("regex " + (string)$2 + " already defined.");
			((Dictionary<string, StateMachine>)yyContext)[(string)$2] = (StateMachine)$4;
		}

regex:	term
		{
			$$ = $1;
		}
	| regex term
		{
			$$ = StateMachine.concatenationMachine((StateMachine)$1, (StateMachine)$2);
		}
	;

term: exp
		{
			$$ = $1;
		}
	| term O exp
		{
			$$ = StateMachine.UnionMachine((StateMachine)$1, (StateMachine)$3);
		}
	;

exp: C action
		{
			$$ = StateMachine.CharMachine((Char)$1, (string)$2);
		}
	| D action
		{
			$$ = StateMachine.CharMachine((Char)0xFFFE, (string)$2);
		}
	| exp S
		{
			$$ = StateMachine.KleenestarMachine((StateMachine)$1);
		}
	| L regex R
		{
			$$ = $2;
		}
	| LC string RC
		{
			if (!((Dictionary<string, StateMachine>)yyContext).ContainsKey((string)$2))
				throw new Exception("regex " + (string)$2 + " has not defined.");
			$$ = ((Dictionary<string, StateMachine>)yyContext)[(string)$2];
		}
	;

action:
		{
			$$ = null;
		}
	| LS multiline_string RS
		{
			$$ = $2;
		}
	;

string: C
		{
			$$ = $1.ToString();
		}
	| string C
		{
			$$ = (string)$1 + (char)$2;
		}
	;

multiline_string:
		{
			$$ = "";
		}
	| multiline_string E
		{
			$$ = (string)$1 + '\n';
		}
	| multiline_string L
		{
			$$ = (string)$1 + '(';
		}
	| multiline_string R
		{
			$$ = (string)$1 + ')';
		}
	| multiline_string LC
		{
			$$ = (string)$1 + '[';
		}
	| multiline_string RC
		{
			$$ = (string)$1 + ']';
		}
	| multiline_string LS
		{
			$$ = (string)$1 + '{';
		}
	| multiline_string CL
		{
			$$ = (string)$1 + ':';
		}
	| multiline_string D
		{
			$$ = (string)$1 + '.';
		}
	| multiline_string S
		{
			$$ = (string)$1 + '*';
		}
	| multiline_string C
		{
			$$ = (string)$1 + (char)$2;
		}
	;

%%
