export interface MovieResponse {
  id: string;
  title: string;
  description: string;
  actors: ActorResponse[];
}

export interface ActorResponse {
  id: string;
  firstName: string;
  lastName: string;
}