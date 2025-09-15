# Load Testing

This folder contains resources for load testing your API using [k6](https://k6.io/).

## Prerequisites

- Install k6: https://k6.io/docs/get-started/installation/

## How to Use

1. Write your load test script (e.g., `script.js`) targeting your API endpoints.
2. Run the test:

	 ```sh
	 k6 run script.js
	 ```

3. Review the k6 output for performance metrics and bottlenecks.

For more details, see the [k6 documentation](https://k6.io/docs/get-started/running-k6/).

## Example

You can use the following as a starting point for your `script.js`:

```js
import http from 'k6/http';
import { check, sleep } from 'k6';

export default function () {
	const res = http.get('http://localhost:5000/api/your-endpoint');
	check(res, { 'status was 200': (r) => r.status === 200 });
	sleep(1);
}
```

---

_Tip: Use load testing to validate your API's scalability and reliability before production deployment._