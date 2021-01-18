import { IGenre } from "./genre.model";
import { IPublisher } from "./publisher.model";

export interface IBook {
  id?: any;
  title?: string;
  description?: string;
  author?: string;
  isbn?: string;
  genre?: IGenre;
  publisher?: IPublisher;
  dateRelease?: Date;
  createAt?: Date;
}
