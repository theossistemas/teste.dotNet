export interface Livro {
    id: number;
    titulo: string;
    autor: string;
    isbn: string;
    anoPublicacao: number;
    quantidade: number;
    preco: number;
    criadoEm?: Date | null;
    atualizadoEm?: Date | null;
    excluidoEm?: Date | null;
    ativo?: boolean;
}