import React, { useState } from 'react';
import { useLazyQuery, useMutation } from '@apollo/client';
import { GET_MOVIE_BY_ID } from "../graphql/queries/movieQueries.ts";
import { CREATE_MOVIE } from "../graphql/mutations/movieMutations.ts";

interface Movie {
  id: number;
  title: string;
  description: string;
  actors: Actor[];
}

interface Actor {
  id: number;
  firstName: string;
  secondName: string;
}

const MainPage: React.FC = () => {
  const [movieId, setMovieId] = useState<number | ''>('');
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');

  const [getMovieById, { loading, error, data }] = useLazyQuery<{ movieById: Movie }>(GET_MOVIE_BY_ID);
  const [createMovie, { loading: creating, error: createError }] = useMutation(CREATE_MOVIE);

  const handleSearch = () => {
    if (movieId !== '') {
      getMovieById({ variables: { id: Number(movieId) } });
    }
  };

  const handleCreate = async () => {
    try {
      await createMovie({
        variables: {
          input: {
            title,
            description,
          },
        },
      });

      alert('Movie created!');
      setTitle('');
      setDescription('');
    } catch {
      // Ошибка уже отобразится ниже
    }
  };

  return (
    <div className="pt-20 px-4 max-w-xl mx-auto">
      <h1 className="text-2xl font-bold text-red-500 mb-4">Find Movie by ID</h1>

      <div className="mb-6 flex gap-2 items-center">
        <input
          type="number"
          placeholder="Enter movie ID"
          value={movieId}
          onChange={(e) => setMovieId(e.target.value === '' ? '' : parseInt(e.target.value))}
          className="border p-2 rounded shadow w-40"
        />
        <button
          onClick={handleSearch}
          className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600"
        >
          Search
        </button>
      </div>

      {loading && <p className="text-gray-600">Loading...</p>}
      {error && <p className="text-red-500">Error: {error.message}</p>}
      {data?.movieById && (
        <div className="border rounded p-4 shadow mb-6">
          <h2 className="text-xl font-semibold">{data.movieById.title}</h2>
          <p className="text-gray-600">{data.movieById.description}</p>

          {data.movieById.actors?.length > 0 && (
            <ul className="mt-2 text-sm text-gray-500">
              {data.movieById.actors.map(actor => (
                <li key={actor.id}>🎬 {actor.firstName}</li>
              ))}
            </ul>
          )}
        </div>
      )}

      <div className="border-t pt-6 mt-6">
        <h2 className="text-xl font-bold text-red-500 mb-4">Create Movie</h2>

        <div className="flex flex-col gap-3 mb-4">
          <input
            type="text"
            placeholder="Title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="border p-2 rounded shadow"
          />
          <textarea
            placeholder="Description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="border p-2 rounded shadow"
          />
        </div>

        <button
          onClick={handleCreate}
          disabled={creating}
          className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700 disabled:opacity-50"
        >
          {creating ? 'Creating...' : 'Create Movie'}
        </button>

        {createError && <p className="text-red-500 mt-2">Error: {createError.message}</p>}
      </div>
    </div>
  );
};

export default MainPage;
