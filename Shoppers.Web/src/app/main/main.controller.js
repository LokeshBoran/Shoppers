(function() {
  'use strict';

  angular
    .module('shoppersWeb')
    .controller('MainController', MainController);

  /** @ngInject */
  function MainController($timeout, webDevTec, toastr, catalogueServiceUrl, pricingServiceUrl, Restangular) {
    var vm = this;

    vm.awesomeThings = [];
    vm.classAnimation = '';
    vm.creationDate = 1467161647968;
    vm.showToastr = showToastr;
    vm.products = [];
    vm.productPricing = [];



      Restangular.allUrl('catalogue', catalogueServiceUrl + '/catalogue').post({
        "Title": "Sample Product",
        "ProductType": "Other"
      });

  
      Restangular.allUrl('price', pricingServiceUrl + '/price/product/1')
     .getList()
     .then(function (data) {
       vm.productPricing = data;

       Restangular.allUrl('price', pricingServiceUrl + '/price').post({
        "ProductId": 1,
        "Provider": "Other",
        "Price": 1.0
      });

     });

     

    activate();

    function activate() {
      getWebDevTec();
      $timeout(function() {
        vm.classAnimation = 'rubberBand';
      }, 4000);
    }

    function showToastr() {
      toastr.info('Fork <a href="https://github.com/Swiip/generator-gulp-angular" target="_blank"><b>generator-gulp-angular</b></a>');
      vm.classAnimation = '';
    }

    function getWebDevTec() {
      vm.awesomeThings = webDevTec.getTec();

      angular.forEach(vm.awesomeThings, function(awesomeThing) {
        awesomeThing.rank = Math.random();
      });
    }
  }
})();
