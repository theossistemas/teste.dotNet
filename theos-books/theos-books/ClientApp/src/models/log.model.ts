export enum ILogType {
  Log = 0,
  Error = 1
}
export interface ILog {
  id?: any;
  message?: string;
  user?: any;
  createAt?: Date;
  type?: ILogType;
}
