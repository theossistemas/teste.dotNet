import { HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

/**
 * Helpers de conversão, formatação, etc.
 */

export class Helpers {
    /** Obtém um ID da rota atual conforme seu nome informado no parâmetro, e converte o ID para inteiro.
     * Returna 0 caso a rota não seja encontrada.
     * {@example Helpers.ObterIdentificadorDaUrl(this.pessoaId, activateRouteSnapshot)}
     */
    static obterIdentificadorDaUrl(nomeIdentificador: string, rota: ActivatedRoute) {
        if (nomeIdentificador.length > 0 && rota) {
            return Number(rota.snapshot.paramMap.get(nomeIdentificador));
        }
        return 0;
    }

    /** Retorna a última URl acessada pelo usuário */
    static retornarParaUltimaUrlAcessada(rota: ActivatedRoute) {
        return rota.snapshot.parent.url[0].path;
    }

    static retornarNomeAcaoFormulario(id: number) {
        if (id === 0) {
            return 'Cadastrar';
        }

        return 'Editar';
    }

    static converterDataParaUTC(data: any): any {
        if (data) {
            return data.split('/').reverse().join('-');
        }
        return data;
    }

    static formatarValorParaReal(valor: number) {
        return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(valor);
    }


    static getHttpHeaders(): HttpHeaders {
        return new HttpHeaders().set('Content-Type', 'application/json');
    }

    static removeNullValues(key, value) {
        if (value !== null) {
            return value;
        }
    }

    static toJson(model: any): any {
        return JSON.stringify(model, this.removeNullValues);
    }

    static convertToCpfCnpj(cpfCnpj) {
        if (cpfCnpj) {
            cpfCnpj = cpfCnpj.toString();
            cpfCnpj = cpfCnpj.replace(/\D/g, '');

            switch (cpfCnpj.length) {
                case 4:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})/, ' $1.$2');
                    break;
                case 5:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})/, ' $1.$2');
                    break;
                case 6:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})/, ' $1.$2');
                    break;
                case 7:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})(\d{3})/, ' $1.$2.$3');
                    break;
                case 8:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})(\d{3})/, ' $1.$2.$3');
                    break;
                case 9:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})(\d{3})/, ' $1.$2.$3');
                    break;
                case 10:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})(\d{3})(\d{1})/, ' $1.$2.$3-$4');
                    break;
                case 11:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})(\d{3})(\d{2})/, ' $1.$2.$3-$4');
                    break;
                case 12:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})(\d{3})(\d{4})/, ' $1.$2.$3/$4');
                    break;
                case 13:
                    cpfCnpj = cpfCnpj.replace(/(\d+)(\d{3})(\d{3})(\d{4})(\d{2})/, ' $1.$2.$3/$4-$5');
                    break;
                case 14:
                    cpfCnpj = cpfCnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d+)/, ' $1.$2.$3/$4-$5');
                    break;
            }
        }
        return cpfCnpj;
    }

    verificaCpf(cpf) {
        if (cpf !== undefined && cpf != null) {
            cpf = cpf.replace(/[^0-9]+/g, '');
            // tslint:disable-next-line: one-variable-per-declaration
            let numeros, digitos, soma, i, resultado, digitosIguais;
            digitosIguais = 1;

            if (cpf.length < 11) {
                return false;
            }

            for (i = 0; i < cpf.length - 1; i++) {
                if (cpf.charAt(i) !== cpf.charAt(i + 1)) {
                    digitosIguais = 0;
                    break;
                }
            }

            if (!digitosIguais) {
                numeros = cpf.substring(0, 9);
                digitos = cpf.substring(9);
                soma = 0;
                for (i = 10; i > 1; i--) {
                    soma += numeros.charAt(10 - i) * i;
                }

                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

                if (resultado !== digitos.charAt(0)) {
                    return false;
                }
                numeros = cpf.substring(0, 10);
                soma = 0;

                for (i = 11; i > 1; i--) {
                    soma += numeros.charAt(11 - i) * i;
                }

                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado !== digitos.charAt(1)) {
                    return false;
                }
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    verificaCnpj(cnpj) {
        if (cnpj !== undefined && cnpj != null) {
            cnpj = cnpj.replace(/[^\d]+/g, '');

            if (cnpj === '') {
                return false;
            }

            if (cnpj.length !== 14) {
                return false;
            }

            // Elimina CNPJs invalidos conhecidos
            if (cnpj === '00000000000000' ||
                cnpj === '11111111111111' ||
                cnpj === '22222222222222' ||
                cnpj === '33333333333333' ||
                cnpj === '44444444444444' ||
                cnpj === '55555555555555' ||
                cnpj === '66666666666666' ||
                cnpj === '77777777777777' ||
                cnpj === '88888888888888' ||
                cnpj === '99999999999999') {
                return false;
            }

            // Valida DVs
            let tamanho = cnpj.length - 2;
            let numeros = cnpj.substring(0, tamanho);
            const digitos = cnpj.substring(tamanho);
            let soma = 0;
            let pos = tamanho - 7;
            let i;

            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }

            let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

            if (resultado !== digitos.charAt(0)) {
                return false;
            }

            tamanho = tamanho + 1;
            numeros = cnpj.substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }

            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado !== digitos.charAt(1)) {
                return false;
            }

            return true;
        } else {
            return false;
        }
    }

    static obterMeses() {
        let meses = [
            { id: 1, nome: 'Janeiro' },
            { id: 2, nome: 'Fevereiro' },
            { id: 3, nome: 'Março' },
            { id: 4, nome: 'Abril' },
            { id: 5, nome: 'Maio' },
            { id: 6, nome: 'Junho' },
            { id: 7, nome: 'Julho' },
            { id: 8, nome: 'Agosto' },
            { id: 9, nome: 'Setembro' },
            { id: 10, nome: 'Outubro' },
            { id: 11, nome: 'Novembro' },
            { id: 12, nome: 'Dezembro' }
        ];

        return meses;
    }

    static obterSiglaEstadoPorNome(nome: string) {
        var data;

        switch (nome.toUpperCase()) {          
            case "ACRE": data = "AC"; break;
            case "ALAGOAS": data = "AL"; break;
            case "AMAZONAS": data = "AM"; break;
            case "AMAPÁ": data = "AP"; break;
            case "BAHIA": data = "BA"; break;
            case "CEARÁ": data = "CE"; break;
            case "DISTRITO FEDERAL": data = "DF"; break;
            case "ESPÍRITO SANTO": data = "ES"; break;
            case "GOIÁS": data = "GO"; break;
            case "MARANHÃO": data = "MA"; break;
            case "MINAS GERAIS": data = "MG"; break;
            case "MATO GROSSO DO SUL": data = "MS"; break;
            case "MATO GROSSO": data = "MT"; break;
            case "PARÁ": data = "PA"; break;
            case "PARAÍBA": data = "PB"; break;
            case "PERNAMBUCO": data = "PE"; break;
            case "PIAUÍ": data = "PI"; break;
            case "PARANÁ": data = "PR"; break;
            case "RIO DE JANEIRO": data = "RJ"; break;
            case "RIO GRANDE DO NORTE": data = "RN"; break;
            case "RONDÔNIA": data = "RO"; break;
            case "RORAIMA": data = "RR"; break;
            case "RIO GRANDE DO SUL": data = "RS"; break;
            case "SANTA CATARINA": data = "SC"; break;
            case "SERGIPE": data = "SE"; break;
            case "SÃO PAULO": data = "SP"; break;
            case "TOCANTÍNS": data = "TO"; break;
        }

        return data;
    }

    obterNomeEstadoPorSigla(uf: string) {
        switch (uf) {
            case "AC": return "Acre";
            case "AL": return "Alagoas";
            case "AM": return "Amazonas";
            case "AP": return "Amapá";
            case "BA": return "Bahia";
            case "CE": return "Ceará";
            case "DF": return "Distrito Federal";
            case "ES": return "Espírito Santo";
            case "GO": return "Goiás";
            case "MA": return "Maranhão";
            case "MG": return "Minas Gerais";
            case "MS": return "Mato Grosso do Sul";
            case "MT": return "Mato Grosso";
            case "PA": return "Pará";
            case "PB": return "Paraíba";
            case "PE": return "Pernambuco";
            case "PI": return "Piauí";
            case "PR": return "Paraná";
            case "RJ": return "Rio de Janeiro";
            case "RN": return "Rio Grande do Norte";
            case "RO": return "Rondônia";
            case "RR": return "Roraima";
            case "RS": return "Rio Grande do Sul";
            case "SC": return "Santa Catarina";
            case "SE": return "Sergipe";
            case "SP": return "São Paulo";
            case "TO": return "Tocantíns";
            default: return "";
        }
    }
}