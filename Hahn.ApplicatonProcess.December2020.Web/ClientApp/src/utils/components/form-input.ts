import {
  bindable,
  customElement,
  bindingMode,
  customAttribute,
  DOM,
  inject,
  autoinject,
} from 'aurelia-framework';

@autoinject
export class InputBox {
  @bindable({ defaultBindingMode: bindingMode.twoWay }) value;
  @bindable type: string;
  @bindable placeholder: string;
  @bindable class: string;
  @bindable id: number;
  @bindable name: string;
  @bindable valueBind: string;
  @bindable readonly: boolean;
}
