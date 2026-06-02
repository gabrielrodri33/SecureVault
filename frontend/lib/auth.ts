const ACCESS_TOKEN_KEY = 'sv_access_token';
const REFRESH_TOKEN_KEY = 'sv_refresh_token';

export const getAccessToken = () => typeof window !== 'undefined' ? localStorage.getItem(ACCESS_TOKEN_KEY) : null;
export const getRefreshToken = () => typeof window !== 'undefined' ? localStorage.getItem(REFRESH_TOKEN_KEY) : null;
export const setTokens = (access: string, refresh: string) => {
  localStorage.setItem(ACCESS_TOKEN_KEY, access);
  localStorage.setItem(REFRESH_TOKEN_KEY, refresh);
};
export const clearTokens = () => {
  localStorage.removeItem(ACCESS_TOKEN_KEY);
  localStorage.removeItem(REFRESH_TOKEN_KEY);
};
