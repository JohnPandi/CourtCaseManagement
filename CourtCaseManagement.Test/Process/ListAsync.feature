#language: pt-br

Funcionalidade: Lista de processos
	Para quanto precisar consultar processo(s)
	Enquanto funcionario
	Eu gostaria de consultar processo(s)

@ProcessListAsync
Esquema do Cenário: Atualizar um processo
	Dado o processo previamente cadastrado
	| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf  | responsiblesName                  | responsiblesEMail   | image      |
	| "3513038-00.2016.8.23.0001" | "2020-10-01"     | "S"           | "C"                  | "teste 01"  | "Em andamento" | "237.958.172-00" | "Nicolas Thales Carlos Moraes"    | "nicolas@gmail.com" | "teste 01" |
	| "3513038-00.2016.8.23.0002" | "2020-10-02"     | "N"           | "C"                  | "teste 02"  | "Em andamento" | "329.080.215-95" | "Silvana Yasmin Adriana Aparício" | "nicolas@gmail.com" | "teste 02" |
	| "3513038-00.2016.8.23.0003" | "2020-10-03"     | "S"           | "C"                  | "teste 03"  | "Desmembrado"  | "460.314.561-68" | "Martin Augusto da Cunha"         | "nicolas@gmail.com" | "teste 03" |
	| "3513038-00.2016.8.23.0004" | "2020-10-04"     | "N"           | "C"                  | "teste 04"  | "Desmembrado"  | "006.147.259-09" | "Raimunda Lívia Louise Sales"     | "nicolas@gmail.com" | "teste 04" |
	| "3513038-00.2016.8.23.0005" | "2020-10-05"     | "S"           | "C"                  | "teste 05"  | "Em recurso"   | "720.951.228-44" | "Pedro Bruno Peixoto"             | "nicolas@gmail.com" | "teste 05" |
	| "3513038-00.2016.8.23.0006" | "2020-10-06"     | "N"           | "C"                  | "teste 06"  | "Em recurso"   | "828.780.131-15" | "Calebe Augusto Porto"            | "nicolas@gmail.com" | "teste 06" |
	| "3513038-00.2016.8.23.0007" | "2020-10-07"     | "S"           | "C"                  | "teste 07"  | "Finalizado"   | "092.552.016-03" | "Carla Caroline Clara da Rosa"    | "nicolas@gmail.com" | "teste 07" |
	| "3513038-00.2016.8.23.0008" | "2020-10-08"     | "N"           | "C"                  | "teste 08"  | "Finalizado"   | "292.604.865-30" | "Cecília Liz Ayla Nogueira"       | "nicolas@gmail.com" | "teste 08" |
	| "3513038-00.2016.8.23.0009" | "2020-10-09"     | "S"           | "C"                  | "teste 09"  | "Arquivado"    | "503.361.196-82" | "Ricardo Augusto Rodrigues"       | "nicolas@gmail.com" | "teste 09" |
	| "3513038-00.2016.8.23.0010" | "2020-10-10"     | "N"           | "C"                  | "teste 10"  | "Arquivado"    | "113.071.090-47" | "Fátima Carolina Castro"          | "nicolas@gmail.com" | "teste 10" |
	| "3513038-00.2016.8.23.0011" | "2020-10-11"     | "S"           | "C"                  | "teste 11"  | "Em andamento" | "634.434.612-47" | "Otávio Sérgio Dias"              | "nicolas@gmail.com" | "teste 11" |
	| "3513038-00.2016.8.23.0012" | "2020-10-12"     | "N"           | "C"                  | "teste 12"  | "Em andamento" | "739.403.782-75" | "Juan Luiz Carvalho"              | "nicolas@gmail.com" | "teste 12" |
	E ao filtrar pelo número do processo unificado <unifiedProcessNumber>
	E ao filtrar pelo periodo de de distribuição <distributionDateStart> <distributionDateEnd>
	E ao filtrar pelo segredo de justiça <justiceSecret>
	E ao filtrar pela pasta física do cliente <clientPhysicalFolder>
	E ao filtrar pela situação para <situationId>
	E ao filtrar pelo nome do responsável <responsiblesName>
	Quando solicitar a consulta dos processos
	Então o sistema retornara o código 200
	E o sistema retornara <qtdeProcessosRetornados> de processos

Exemplos:
| unifiedProcessNumber        | distributionDateStart | distributionDateEnd | justiceSecret | clientPhysicalFolder | situationId    | responsiblesName               | qtdeProcessosRetornados |
| "3513038-00.2016.8.23.0001" | ""                    | ""                  | ""            | ""                   | ""             | ""                             | 1                       |
| ""                          | "2020-10-01"          | "2020-10-01"        | ""            | ""                   | ""             | ""                             | 1                       |
| ""                          | "2020-10-01"          | "2020-10-02"        | ""            | ""                   | ""             | ""                             | 2                       |
| ""                          | ""                    | ""                  | "S"           | ""                   | ""             | ""                             | 6                       |
| ""                          | ""                    | ""                  | "N"           | ""                   | ""             | ""                             | 6                       |
| ""                          | ""                    | ""                  | ""            | "C"                  | ""             | ""                             | 12                      |
| ""                          | ""                    | ""                  | ""            | ""                   | "Em andamento" | ""                             | 4                       |
| ""                          | ""                    | ""                  | ""            | ""                   | "Desmembrado"  | ""                             | 2                       |
| ""                          | ""                    | ""                  | ""            | ""                   | ""             | "Nicolas Thales Carlos Moraes" | 1                       |
| ""                          | ""                    | ""                  | ""            | ""                   | ""             | "Ca"                           | 6                       |