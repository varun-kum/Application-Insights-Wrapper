# Application-Insights-Wrapper
Wrapper to Implement End-to-End transaction tracking on AppInsights using Requests and Dependencies

This Wrapper Helps to manage end to end transaction in the system using the transaction id to log into AppInsights.

For example: A web request on app service enques data into queue and is being consumed by an Job to save into database etc. With the help of Requesst ID and creating dependencies in each module you can view the application maps and flow on AppInsights to track the transaction.
