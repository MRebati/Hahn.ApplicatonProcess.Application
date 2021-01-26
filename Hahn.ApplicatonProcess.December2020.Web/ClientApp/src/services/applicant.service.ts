import { ApiService } from "./../infrastructure/apiService";
import { HttpClient, json } from "aurelia-fetch-client";
import { inject } from "aurelia-framework";
import { Applicant } from "../models/applicant";

@inject(HttpClient)
export class ApplicantService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getApplicantById(applicantId: number): Promise<Applicant> {
    return this.http
      .fetch(`applicant/${applicantId}`)
      .then((response) => response.json())
      .then((items) => {
        return items;
      })
      .catch((error) => {
        console.log(
          `an error occured on getting the applicant with id ${applicantId}.`,
          error
        );
        return [];
      });
  }

  getApplicantList(): Promise<Array<Applicant>> {
    return this.http
      .fetch("applicant", { method: "get" })
      .then((response) => response.json())
      .then((applicants) => {
        return applicants;
      })
      .catch((error) => {
        console.log("an error occured on retrieving applicant.", error);
        return [];
      });
  }

  insertApplicant(applicant: Applicant): any {
    return this.http
      .fetch("applicant", {
        method: "post",
        body: json(applicant),
      })
      .then((response) => response.json())
      .then((responseMessage) => {
        return responseMessage;
      })
      .catch((error) => {
        console.log("an error occured on adding applicant", error);
      });
  }

  updateApplicant(applicant: Applicant): any {
    return this.http
      .fetch(`applicant/${applicant.id}`, {
        method: "put",
        body: json(applicant),
      })
      .then((response) => response.json())
      .then((responseMessage) => {
        return responseMessage;
      })
      .catch((error) => {
        console.log("an error occured on updating applicant", error);
      });
  }

  deleteApplicant(id: number): any {
    return this.http
      .fetch(`applicant/${id}`, {
        method: "delete",
      })
      .then((response) => response.json())
      .then((responseMessage) => {
        return responseMessage;
      })
      .catch((error) => {
        console.log("an error occured on deleting applicant", error);
      });
  }
}
