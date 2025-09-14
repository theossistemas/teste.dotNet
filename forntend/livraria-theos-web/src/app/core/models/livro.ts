export interface Livro {
  id?: string;
  titulo: string;
  autor: string;
  anoPublicacao?: number | null;
  isbn?: string | null;
  editora?: string | null;
  dataPublicacao: string; // yyyy-MM-dd
  preco: number;
  estoque: number;
}

export interface PageResult<T> {
  total: number;
  page: number;
  pageSize: number;
  itens: T[];
}
