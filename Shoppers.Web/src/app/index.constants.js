/* global malarkey:false, moment:false */
(function() {
  'use strict';

  angular
    .module('shoppersWeb')
    .constant('malarkey', malarkey)
    .constant('moment', moment)
    .constant('pricingServiceUrl', 'http://localhost:5001/api')
    .constant('catalogueServiceUrl', 'http://localhost:5000/api');

})();
