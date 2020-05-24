import { Author } from "./Author";
import { Publisher } from "./Publisher";

export class Book {
  id: number;
  title: string;
  iSBN: string;
  authors: Author[];
  pageCount: number;
  publisher: Publisher;
  year: number;
  edition: number;
  city: string;
}
