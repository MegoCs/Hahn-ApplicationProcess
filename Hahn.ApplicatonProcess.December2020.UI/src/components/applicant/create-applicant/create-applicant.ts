import { Applicant } from "../models/Applicant"
import { inject, NewInstance } from 'aurelia-framework';
import { ValidationRules, ValidationController } from "aurelia-validation";

@inject(NewInstance.of(ValidationController))
export class CreateApplicant {
  public applicant: Applicant= new Applicant;
  constructor(private controller: ValidationController) {
    this.configureValidation();
  }
  configureValidation() {

    ValidationRules.customRule(
      'validCountry',
      (value, obj) => value === null || value === undefined
        ||
        //call country api instead of true
        true,
      `This is not a valid country`
    );

    ValidationRules
      .ensure((m: Applicant) => m.name)
      .required()
      .minLength(5)
      .ensure((m: Applicant) => m.familyName)
      .required()
      .minLength(5)
      .ensure((m: Applicant) => m.address)
      .required()
      .minLength(10)
      .ensure((m: Applicant) => m.countryOfOrigin)
      .required()
      .satisfiesRule('validCountry')
      .ensure((m: Applicant) => m.emailAddress)
      .required()
      //.matches(RegExp(".*@.*[.][com,net,org,gov]"))
      .ensure((m: Applicant) => m.age)
      .required()
      .between(19, 61)
      .on(this.applicant);

  }
  
  get sendDisabled () {
    this.controller.validate();
    return this.controller.errors.length!=0;
  };

  get resetDisabled(){
    return false;
  }
  
  send(){

  }
  reset(){
    //show reset dialog and reset the form
    // are you sure you need to reset all the data ?
  }
}
