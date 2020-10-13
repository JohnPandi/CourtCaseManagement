#language: pt-br

Funcionalidade: Responsáveis
	Para quanto precisar cadastrar um responsável
	Enquanto funcionario
	Eu gostaria de cadastrar um responsável

@ResponsibleAddAsync
Esquema do Cenário: Cadastrar novo responsável
	Dado o cadastro de um novo responsável com o cpf <responsiblesCpf>
	E com o nome <responsiblesName>
	E com o e-mail <responsiblesEMail>
	E com a foto <image>
	Quando solicitar o cadastro do responsável
	Então o sistema retornara o código 201

Exemplos:
|responsiblesCpf  | responsiblesName                | responsiblesEMail   | image   |
|"237.958.172-00" | "Nicolas Thales Carlos Moraes"  | "nicolas@gmail.com" | "teste" |
|"558.526.150-99" | "Murilo Miguel Benício Ribeiro" | "murilo@gmail.com"  | "teste" |

@ResponsibleAddAsync
Esquema do Cenário: Cadastrar novo responsável sem informar todos os campos obrigatórios
	Dado o cadastro de um novo responsável com o cpf <responsiblesCpf>
	E com o nome <responsiblesName>
	E com o e-mail <responsiblesEMail>
	E com a foto <image>
	Quando solicitar o cadastro do responsável
	Então o sistema retornara o código 400
	E apresentara os seguintes codigo de erros <messages>

Exemplos:
| responsiblesCpf | responsiblesName               | responsiblesEMail   | image   | messages                                                   |
| ""              | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" | "RequiredCpf"                                              |
| ""              | ""                             | "nicolas@gmail.com" | "teste" | "RequiredCpf;RequiredName"                                 |
| ""              | ""                             | ""                  | "teste" | "RequiredCpf;RequiredName;RequiredMail"                    |
| ""              | ""                             | ""                  | ""      | "RequiredCpf;RequiredName;RequiredMail;RequiredPhotograph" |
