import { gql } from '@apollo/client';

export const CREATE_MOVIE = gql`
  mutation CreateMovie($input: CreateMovieInput!) {
    createMovie(input: $input) {
      id
      title
      description
      actors {
        id
        firstName
      }
    }
  }
`;

export const DELETE_MOVIE = gql`
  mutation DeleteMovie($id: Int!) {
    deleteMovie(id: $id)
  }
`;