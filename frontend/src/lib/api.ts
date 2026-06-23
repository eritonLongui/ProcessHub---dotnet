const API_BASE_URL = 'http://localhost:5008/api'; // porta http C#

export async function fetchApi<T>(endpoint: string, options?: RequestInit): Promise<T> {
  const url = `${API_BASE_URL}${endpoint}`;
  
  const defaultHeaders: Record<string, string> = {
    'Content-Type': 'application/json',
  };

  // Se estivermos no lado do cliente, podemos pegar o token do localStorage
  if (typeof window !== 'undefined') {
    const token = localStorage.getItem('token');
    // console.log("TOKEN LIDO DO STORAGE:", token);
    if (token) {
      defaultHeaders['Authorization'] = `Bearer ${token}`;
    } else {
    //   console.log("NENHUM TOKEN ENCONTRADO NO STORAGE!");
    }
  }

  const response = await fetch(url, {
    ...options,
    headers: {
      ...defaultHeaders,
      ...options?.headers,
    },
  });

  if (!response.ok) {
    throw new Error(`Erro na API: ${response.statusText}`);
  }

  return response.json();
}