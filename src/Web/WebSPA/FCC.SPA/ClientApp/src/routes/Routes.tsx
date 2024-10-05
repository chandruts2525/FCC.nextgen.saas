import React, { lazy, Suspense } from 'react';
import { Route } from 'react-router';
import { Routes } from 'react-router-dom';
import { Loader } from '../components/CommonComponent';

const EmployeeListing = lazy(() => import('../components/EmployeeListing'));
const JobTypes = lazy(() => import('../components/JobTypes'));
const QuoteFooter = lazy(() => import('../components/QuoteFooter'));
const RoleListing = lazy(() => import('../components/RoleListing'));
const ScheduleListing = lazy(() => import('../components/ScheduleListing'));
const UnitMeasure = lazy(() => import('../components/UnitMeasure'));
const UserListing = lazy(() => import('../components/UserListing'));
const OrganizationSetting = lazy(() => import('../components/OrganizationSetting'))

const RoutesComponent = () => {
  return (
    <Routes>
      <Route
        path="/"
        element={
          <Suspense fallback={<Loader />}>
            <RoleListing />
          </Suspense>
        }
          />
          <Route
              path="/RoleListing"
              element={
                  <Suspense fallback={<Loader />}>
                      <RoleListing />
                  </Suspense>
              }
          />
      <Route
        path="/UserListing"
        element={
          <Suspense fallback={<Loader />}>
            <UserListing />
          </Suspense>
        }
      />
      <Route
        path="/JobTypes"
        element={
          <Suspense fallback={<Loader />}>
            <JobTypes />
          </Suspense>
        }
      />
      <Route
        path="/ScheduleListing"
        element={
          <Suspense fallback={<Loader />}>
            <ScheduleListing />
          </Suspense>
        }
      />
      <Route
        path="/EmployeeListing"
        element={
          <Suspense fallback={<Loader />}>
            <EmployeeListing />
          </Suspense>
        }
      />
      <Route
        path="/QuoteFooters"
        element={
          <Suspense fallback={<Loader />}>
            <QuoteFooter />
          </Suspense>
        }
      />
      <Route
        path="/UnitOfMeasure"
        element={
          <Suspense fallback={<Loader />}>
            <UnitMeasure />
          </Suspense>
        }
      />
      <Route
        path="/OrganizationSetting"
        element={
          <Suspense fallback={<Loader />}>
            <OrganizationSetting/>
          </Suspense>
        }
      />
    </Routes>
  );
};

export default RoutesComponent;
