import { autoinject } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";

@autoinject
export class DialogComponent {
  constructor(public controller: DialogController) {
    controller.settings.centerHorizontalOnly = true;
  }

  message = '';
  
  activate(data: string): void {
    this.message = data;
  }
  
  ok(): void {
    this.controller.ok();
  }
  
  cancel(): void {
    this.controller.cancel();
  }
}
