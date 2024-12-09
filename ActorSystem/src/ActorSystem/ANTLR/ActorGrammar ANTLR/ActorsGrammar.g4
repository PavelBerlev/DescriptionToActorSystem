grammar ActorsGrammar;
/* правила лексера */
actorSystem : actor+;

/*Обновлять по мере появления акторов*/
actor : (dbwriter | tasknotion);
dbwriter : DBWRITER NAME WORD COLLECTION WORD (NEXT WORD)? KEYS keyValue+;
tasknotion : TASKNOTION NAME WORD COLLECTION WORD;
keyValue : KEY WORD;


/* правила лексера */
fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
INTEGER : '0' | [1-9] [0-9]*;
DBWRITER : 'DBWriter';
TASKNOTION : 'TaskNotion';
NAME : 'Name';
NEXT : 'Next';
KEYS : 'Keys';
KEY : 'Key';
NOTIONTO : 'NotionTo';
COLLECTION : 'Collection';
WORD: (LOWERCASE | UPPERCASE)+;
WHITESPACE: (' ' | '\t')+ -> skip;
NEWLINE   : ('\r'? '\n' | '\r')+;


