import type { AxiosError } from "axios";
import api from "../utils/api.ts";
import type {MovieResponse} from "../models/MovieResponse.ts";
import type {ErrorResponse} from "../models/ErrorResponse.ts";

export const getMovieById = async (id: string): Promise<MovieResponse> => {
  try {
    const response = await api.get<MovieResponse>(`/movies/${id}`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response) {
      if (error.response.status === 404 && error.response.data?.error) {
        throw new Error(error.response.data.error);
      }
      if (error.response.status === 429) {
        throw new Error("Вы превысили лимит запросов");
      }
    }

    throw new Error("Couldn't get movie");
  }
};