#language: pt-br

Funcionalidade: Atualizar Responsável
	Para quanto precisar atualizar um responsável
	Enquanto funcionario
	Eu gostaria de atualizar os dados de um responsável

@ResponsibleUpdateAsync
Esquema do Cenário: Atualizar um responsável
	Dado um responsável previamente cadastrado
	| responsiblesName               | responsiblesEMail   | image   | responsiblesCpf  |
	| "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" | "237.958.172-00" |
	E atualizando o cpf para <responsiblesCpf>
	E atualizando o nome para <responsiblesName>
	E atualizando o e-mail para <responsiblesEMail>
	E atualizando a foto para <image>
	Quando solicitar o atualizar um responsável
	Então o sistema retornara o código 200

Exemplos:
| responsiblesCpf  | responsiblesName               | responsiblesEMail   | image   |
| "303.951.780-53" | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" |

@ResponsibleUpdateAsync
Esquema do Cenário: Atualizar um responsável sem informar todos os campos obrigatórios
	Dado um responsável previamente cadastrado
	| responsiblesCpf  | responsiblesName               | responsiblesEMail   | image   |
	| "237.958.172-00" | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" |
	E atualizando o cpf para <responsiblesCpf>
	E atualizando o nome para <responsiblesName>
	E atualizando o e-mail para <responsiblesEMail>
	E atualizando a foto para <image>
	Quando solicitar o atualizar um responsável
	Então o sistema retornara o código 400
	E apresentara os seguintes codigo de erros <messages>

Exemplos:
| responsiblesCpf | responsiblesName               | responsiblesEMail   | image   | messages                                                   |
| ""              | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" | "RequiredCpf"                                              |
| ""              | ""                             | "nicolas@gmail.com" | "teste" | "RequiredCpf;RequiredName"                                 |
| ""              | ""                             | ""                  | "teste" | "RequiredCpf;RequiredName;RequiredMail"                    |
| ""              | ""                             | ""                  | ""      | "RequiredCpf;RequiredName;RequiredMail;RequiredPhotograph" |
