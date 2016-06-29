(function() {
  'use strict';

  angular
    .module('shoppersWeb')
    .run(runBlock);

  /** @ngInject */
  function runBlock($log) {

    $log.debug('runBlock end');
  }

})();
