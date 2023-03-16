pipeline {
   agent any

   stages {
      stage('Build') {
         steps {
            sh 'dotnet build'
         }
      }
      stage('Test') {
         steps {
            sh 'dotnet test'
         }
      }
      stage('Publish') {
         steps {
            sh 'dotnet publish'
         }
      }
   }
}
