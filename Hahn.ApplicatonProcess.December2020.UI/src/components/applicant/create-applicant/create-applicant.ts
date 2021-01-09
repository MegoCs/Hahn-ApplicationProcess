import {Applicant} from "../models/Applicant"

export class CreateApplicant {
  message: string;
  applicant:Applicant;
  constructor() {
    this.message = 'Hello world Applicant';
  }
  addApplicant(){
    console.log(this.applicant);
  }
}
