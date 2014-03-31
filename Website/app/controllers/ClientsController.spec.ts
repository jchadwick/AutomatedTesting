/// <chutzpah_reference path="../../scripts/angular.js" />
/// <chutzpah_reference path="../../scripts/angular-route.js" />
/// <chutzpah_reference path="../../scripts/angular-mocks.js" />
/// <reference path="../../scripts/typings/jasmine/jasmine.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-mocks.d.ts" />
/// <reference path="../app.ts" />
/// <reference path="../services/ClientsService.ts" />
/// <reference path="ClientsController.ts" />

describe("ClientsController", function () {

    beforeEach(module('AutomatedTestingApp'));

    it('should load clients from ClientsService on initialization', inject(function ($q, $controller) {


        var expectedClients = [{ Id: 1, Name: "Test Client #1" }],
            deferred = $q.defer(),
            getClientsPromise = deferred.promise,
            $scope = <AutomatedTesting.ClientsViewModel>{ clients: null },
            $inject = {
                $log: jasmine.createSpy('$log'),
                $scope: $scope,
                clientsService: <AutomatedTesting.IClientsService>{ 
                    getClient: function () { return null; },
                    getClients: function() {
                        console.log('Loading')
                        deferred.resolve(expectedClients);
                        return getClientsPromise;
                    }
                }
            };

        $controller('ClientsController', $inject);

        getClientsPromise.then(function() {
            expect($scope.clients).toBe(expectedClients);
        })
    }))


})