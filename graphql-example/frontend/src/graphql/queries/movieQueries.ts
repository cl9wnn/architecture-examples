import { gql } from '@apollo/client';

export const GET_ALL_MOVIES = gql`
  query GetAllMovies {
    allMovies {
      id
      title
      description
    }
  }
`;

export const GET_MOVIE_BY_ID = gql`
  query GetMovieById($id: Int!) {
    movieById(id: $id) {
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