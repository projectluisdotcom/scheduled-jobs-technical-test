# Technical Test Instructions

## Functional

At {COMPANY NAME} we deal with high volumes of data to be processed for migration from a legacy core banking application to our cloud core banking platform. For this, clients are expected to use our bulk and batch API's. These API's validate, process, and return statuses. All to enable the clients to test and perform migration of data.

You are asked to develop a service that will allow our clients to process their data in our infrastructure. The service must be delivered as a **REST API** to allow easy integration to other systems. Also, it is required to have the following features for the consumers:

- As a consumer I want to START A TYPE OF JOB so that my data can be processed. For the moment, we support:
  - BULK jobs: Processes all the provided data in sequence. For this, the process should not stop if one fails. There is no rollback.
  - BATCH jobs: Processes all the provided data in sequence. For this, the process stops, if one fails. There is no rollback.
  - We expect more types of jobs in the future.
- As a consumer I want to CHECK THE STATUS OF A JOB so that I can see its progress.
- As a consumer I want to GET LOGS FROM A JOB so that I can see everything has gone as expected or check the errors.

## Technical

Another team is working on this project and they will implement the service responsible for the job data processing. They told you that, on average, it might take 500 milliseconds to process one single element.

The service must be implemented as if it is production ready. So, it should:

- Allow to easily trace any problem it might have during the execution.
- Scale properly under large number of requests.

## Evaluation

Your teammates are going to review your work and they provided a list of things they look when doing code reviews:

- Follow and demonstrate the SOLID principles.
- Use appropriate design patterns and at least show use of dependency injection.
- Show approach of test driven design and high usability of unit testing.
- Please return your results within 4 hours of receiving this email.
