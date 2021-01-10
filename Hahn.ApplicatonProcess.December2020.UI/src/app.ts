import { PLATFORM } from 'aurelia-pal';
import { RouterConfiguration, Router } from "aurelia-router";
import {I18N} from 'aurelia-i18n';

export class App {
  configureRouter(config: RouterConfiguration, router: Router) {
    config.options.pushState = true;
    config.map([
      { route: ['', 'Applicant/:langUrl'],name: 'Applicant', title: 'Applicants', moduleId: PLATFORM.moduleName('components/applicant/create-applicant/create-applicant'), nav: true },
      { route: ['ApplicantResponse'], name: 'Applicant', title: 'Applicants', moduleId: PLATFORM.moduleName('components/applicant/applicant-response/applicant-response'), nav: true }    ]);  
  }
  static inject = [I18N];
    constructor(private i18n:I18N) {
      const urlParams = new URLSearchParams(window.location.search);
      const myParam = urlParams.get('lang');
      this.i18n.setLocale(myParam||'en');
    }
}
