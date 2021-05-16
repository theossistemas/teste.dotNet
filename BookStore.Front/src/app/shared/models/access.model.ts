import uuid from 'uuid';

export class Access {
  id: uuid;
  email: string;
  jwtToken: string;
  name: string;
  role: number;
}

export enum Role {
  ADMIN = 0,
  USER = 1,
}
