export class Estado {
    uf: string;
    nomeUf: string;

    static getEstados(): Estado[] {
        return [
            { uf: 'AC', nomeUf: 'Acre' },
            { uf: 'AL', nomeUf: 'Alagoas' },
            { uf: 'AP', nomeUf: 'Amapá' },
            { uf: 'AM', nomeUf: 'Amazonas' },
            { uf: 'BA', nomeUf: 'Bahia' },
            { uf: 'CE', nomeUf: 'Ceará' },
            { uf: 'DF', nomeUf: 'Distrito Federal' },
            { uf: 'ES', nomeUf: 'Espírito Santo' },
            { uf: 'GO', nomeUf: 'Goiás' },
            { uf: 'MA', nomeUf: 'Maranhão' },
            { uf: 'MT', nomeUf: 'Mato Grosso' },
            { uf: 'MS', nomeUf: 'Mato Grosso do Sul' },
            { uf: 'MG', nomeUf: 'Minas Gerais' },
            { uf: 'PA', nomeUf: 'Pará' },
            { uf: 'PB', nomeUf: 'Paraíba' },
            { uf: 'PR', nomeUf: 'Paraná' },
            { uf: 'PE', nomeUf: 'Pernambuco' },
            { uf: 'PI', nomeUf: 'Piauí' },
            { uf: 'RJ', nomeUf: 'Rio de Janeiro' },
            { uf: 'RN', nomeUf: 'Rio Grande Do Norte' },
            { uf: 'RS', nomeUf: 'Rio Grande Do Sul' },
            { uf: 'RO', nomeUf: 'Rondônia' },
            { uf: 'RR', nomeUf: 'Roraima' },
            { uf: 'SC', nomeUf: 'Santa Catarina' },
            { uf: 'SP', nomeUf: 'São Paulo' },
            { uf: 'SE', nomeUf: 'Sergipe' },
            { uf: 'TO', nomeUf: 'Tocantins' }
        ];
    }
}
