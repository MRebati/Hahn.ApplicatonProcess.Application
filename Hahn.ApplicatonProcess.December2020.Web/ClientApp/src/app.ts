import { InsertApplicantComponent } from './components/insert-applicant-component/insert.applicant.component';
import { Router, RouterConfiguration } from "aurelia-router";
import { PLATFORM } from "aurelia-framework";


export class App {
  //variables
  router: Router;

  // config router
  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = "Applicant Project";
    config.options.pushState = true;
    //config.options.root = "/";
    config.map([
      {
        route: ["", "add"],
        name: "insert",
        moduleId: PLATFORM.moduleName("components/insert-applicant-component/insert.applicant.component"),
        title: "Add New Applicant",
        nav: true,
      },

      {
        route: ["list"],
        name: "list",
        moduleId: PLATFORM.moduleName("components/list-applicant-component/list.applicant.component"),
        title: "Applicant List",
        nav: true,
      },

      {
        route: ["update/:id"],
        name: "update",
        title: "Update Applicant",
        moduleId: PLATFORM.moduleName("components/update-applicant-component/update.applicant.component"),
      },
    ]);

    this.router = router;
  }
}
