import type { AxiosError } from 'axios';
import type { ErrorResponse } from "../models/ErrorResponse";
import api from "../utils/api.ts";

interface CreateMovieRequest {
  title: string;
  description: string;
}

export const createMovie = async (input: CreateMovieRequest): Promise<void> => {
  try {
    await api.post('/movies', input);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Couldn't create movie");
  }
};