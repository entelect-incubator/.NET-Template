import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '30s', target: 10 }, // Ramp-up to 10 users over 30 seconds
        { duration: '1m', target: 10 },  // Stay at 10 users for 1 minute
        { duration: '30s', target: 0 },  // Ramp-down to 0 users over 30 seconds
    ],
};

const BASE_URL = 'https://your-api-endpoint.com'; // Replace with your API's base URL
const headers = { 'Content-Type': 'application/json' };

export default function () {
    // Create Pizza
    let createPayload = JSON.stringify({
        name: 'Margherita',
        ingredients: ['Tomato', 'Mozzarella', 'Basil'],
        price: 8.99,
    });

    let createResponse = http.post(`${BASE_URL}/pizzas`, createPayload, { headers });
    check(createResponse, { 'Create pizza status was 200': (r) => r.status === 200 });
    
    // Search Pizza
    let searchPayload = JSON.stringify({
        name: 'Margherita',
    });

    let searchResponse = http.post(`${BASE_URL}/pizzas/search`, searchPayload, { headers });
    check(searchResponse, { 'Search pizza status was 200': (r) => r.status === 200 });

    // Update Pizza
    let updatePayload = JSON.stringify({
        id: 1, // Replace with actual ID of pizza
        name: 'Margherita Deluxe',
        ingredients: ['Tomato', 'Mozzarella', 'Basil', 'Parmesan'],
        price: 10.99,
    });

    let updateResponse = http.put(`${BASE_URL}/pizzas`, updatePayload, { headers });
    check(updateResponse, { 'Update pizza status was 200': (r) => r.status === 200 });

    sleep(1);
}
