import {autoinject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@autoinject
export class Prompt {
    message: any;
   constructor(private controller:DialogController) {
      controller.settings.centerHorizontalOnly = true;
   }
   activate(message) {
      this.message = message;
   }
}