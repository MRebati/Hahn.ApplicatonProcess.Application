import { configuration } from "./config";
import { HttpClient } from "aurelia-fetch-client";

export class ApiService {
  public http: HttpClient;
  constructor(http: HttpClient) {
    http.configure((config) => {
      config
        .withBaseUrl(configuration.apiUrl)
        .withDefaults({
          headers: {
            Accept: "application/json",
            "X-Requested-With": "Fetch",
          },
        })
        .withInterceptor({
          request(request) {
            console.log(`Requesting ${request.method} ${request.url}`);
            return request; // you can return a modified Request, or you can short-circuit the request by returning a Response
          },
          response(response) {
            console.log(`Received ${response.status} ${response.url}`);
            return response; // you can return a modified Response
          },
        });
    });
    this.http = http;
  }
}
