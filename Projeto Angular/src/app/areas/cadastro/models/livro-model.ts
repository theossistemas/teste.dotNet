import { Autor } from './autor-model';
import { Genero } from './genero-model';
export class Livro {
    id: number;
    nome: string;
    quantidadePaginas: number;
    sinopse: string;
    ativo: string;
    
    genero: Genero;
    autor: Autor;
}