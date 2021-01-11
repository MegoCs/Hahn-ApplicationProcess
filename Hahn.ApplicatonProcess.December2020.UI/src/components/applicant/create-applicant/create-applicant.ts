import { Applicant } from "../models/Applicant"
import { inject, NewInstance } from 'aurelia-framework';
import { ValidationRules, ValidationController, validationMessages } from "aurelia-validation";
import { BootstrapFormRenderer } from '../../../core/renderers/bootstrapformrenderer'
import { I18N } from "aurelia-i18n";
import { autoinject } from 'aurelia-framework';
import { DialogService } from 'aurelia-dialog';
import { ApplicantService } from "../applicant-service"
import {Router} from 'aurelia-router';

@autoinject
export class CreateApplicant {
  public applicant: Applicant = new Applicant;
  constructor(private router:Router,private applicantService: ApplicantService, private i18n: I18N, private controller: ValidationController, private dialogService: DialogService) {
    this.i18n.i18nextReady().then(() => {
      this.configureValidation();
    });

  }
  configureValidation() {

    this.controller.addRenderer(new BootstrapFormRenderer());
    ValidationRules.customRule(
      'validCountry',
      (value, obj) => value === null || value === undefined
        ||
        //call country api instead of true
        true,
      `\${$value} ` + this.i18n.tr('CountryValidationMessage')
    );

    validationMessages['RequiredValidationMessage'] = `\${$displayName} ` + this.i18n.tr('RequiredValidationMessage');
    validationMessages['CountryValidationMessage'] = `\${$value} ` + this.i18n.tr('CountryValidationMessage');
    validationMessages['AgeValidationMessage'] = this.i18n.tr('AgeValidationMessage');
    validationMessages['LengthValidationMessage'] = this.i18n.tr('LengthValidationMessage') + `\${$config.length}`;
    validationMessages['EmailalidationMessage'] = `\${$value} ` + this.i18n.tr('EmailalidationMessage');

    ValidationRules
      .ensure((m: Applicant) => m.name).displayName(this.i18n.tr('Name'))
      .required().withMessageKey('RequiredValidationMessage')
      .minLength(5).withMessageKey('LengthValidationMessage')

      .ensure((m: Applicant) => m.familyName).displayName(this.i18n.tr('FamilyName'))
      .required().withMessageKey('RequiredValidationMessage')
      .minLength(5).withMessageKey('LengthValidationMessage')

      .ensure((m: Applicant) => m.address).displayName(this.i18n.tr('Address'))
      .required().withMessageKey('RequiredValidationMessage')
      .minLength(10).withMessageKey('LengthValidationMessage')

      .ensure((m: Applicant) => m.countryOfOrigin).displayName(this.i18n.tr('CountryOfOrigin'))
      .required().withMessageKey('RequiredValidationMessage')
      .satisfiesRule('validCountry').withMessageKey('CountryValidationMessage')

      .ensure((m: Applicant) => m.emailAddress)
      .required().withMessageKey('RequiredValidationMessage')
      .matches(RegExp(".*@.*\.(com|net|org|gov)$")).withMessageKey('EmailalidationMessage')
      .ensure((m: Applicant) => m.age)
      .required().withMessageKey('RequiredValidationMessage')
      .between(19, 61).withMessageKey('AgeValidationMessage')
      .on(this.applicant);

  }

  get sendDisabled() {
    return this.controller.errors.length != 0 ;
  };

  get resetDisabled() {
    return false;
  }

  send() {
    this.applicantService.sendApplicantData(this.applicant)
      .then(suc => {
        this.router.navigate("applicantResponse");
      })
      .catch(err => {
        console.log(err);
      });
  }
  reset() {
    //show reset dialog and reset the form
    // are you sure you need to reset all the data ?

    // this.dialogService.open({ viewModel: Prompt, model: 'Good or Bad?', lock: false }).whenClosed(response => {
    //   if (!response.wasCancelled) {
    //     console.log('good');
    //   } else {
    //     console.log('bad');
    //   }
    //   console.log(response.output);
    // });
  }
}
