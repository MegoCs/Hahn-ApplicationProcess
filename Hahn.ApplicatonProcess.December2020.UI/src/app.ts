import { PLATFORM } from 'aurelia-pal';
import { RouterConfiguration, Router } from "aurelia-router";

export class App {
  public message = 'Hello World!';
  configureRouter(config: RouterConfiguration, router: Router) {
    config.options.pushState = true;
  
    config.map([
      { route: ['', 'Applicant'], name: 'Applicant', title: 'Applicants', moduleId: PLATFORM.moduleName('components/applicant/create-applicant/create-applicant'), nav: true },
      { route: ['ApplicantResponse'], name: 'Applicant', title: 'Applicants', moduleId: PLATFORM.moduleName('components/applicant/applicant-response/applicant-response'), nav: true }    ]);  
  }
}
