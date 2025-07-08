import React, { useState } from 'react';
import {getMovieById} from "../services/getMovieById.ts";
import type {MovieResponse} from "../models/MovieResponse.ts";
import {createMovie} from "../services/createMovie.ts";

const MainPage: React.FC = () => {
  const [movieId, setMovieId] = useState<number | ''>('');
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');

  const [movieData, setMovieData] = useState<MovieResponse | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [creating, setCreating] = useState(false);
  const [createError, setCreateError] = useState<string | null>(null);

  const handleSearch = async () => {
    if (movieId === '') return;

    setLoading(true);
    setError(null);
    setMovieData(null);

    try {
      const data = await getMovieById(String(movieId));
      setMovieData(data);
    } catch (err) {
      setError((err as Error).message);
    } finally {
      setLoading(false);
    }
  };

  const handleCreate = async () => {
    setCreating(true);
    setCreateError(null);

    try {
      await createMovie({ title, description });
      alert('Movie created!');
      setTitle('');
      setDescription('');
    } catch (err) {
      setCreateError((err as Error).message);
    } finally {
      setCreating(false);
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
      {error && <p className="text-red-500">Error: {error}</p>}
      {movieData && (
        <div className="border rounded p-4 shadow mb-6">
          <h2 className="text-xl font-semibold">{movieData.title}</h2>
          <p className="text-gray-600">{movieData.description}</p>

          {movieData.actors?.length > 0 && (
            <ul className="mt-2 text-sm text-gray-500">
              {movieData.actors.map(actor => (
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

        {createError && <p className="text-red-500 mt-2">Error: {createError}</p>}
      </div>
    </div>
  );
};

export default MainPage;
