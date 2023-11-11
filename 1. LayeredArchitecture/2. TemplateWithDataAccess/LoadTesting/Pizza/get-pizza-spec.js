import http from 'k6/http';
import { sleep } from 'k6';

const url = 'https://localhost:44353/Pizza/Search';

export const options = {
  stages: [
    { duration: '30s', target: 20 },
    { duration: '1m30s', target: 10 },
    { duration: '20s', target: 0 },
  ],
};

export default function getSamplesFlow() {
  const data = {};

  // Using a JSON string as body
  let res = http.request('POST', url, JSON.stringify(data), {
    headers: { 'Content-Type': 'application/json' },
  });
  console.log(res.json());
}