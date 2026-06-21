const API_BASE_URL = 'http://localhost:5008'; // porta http C#

export async function fetchApi<T>(endpoint: string, options?: RequestInit): Promise<T> {
  const url = `${API_BASE_URL}${endpoint}`;
  
  const defaultHeaders = {
    'Content-Type': 'application/json',
    // Aqui no futuro você adicionará o token:
    // 'Authorization': `Bearer ${localStorage.getItem('token')}`
  };

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