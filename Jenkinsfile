pipeline {
  agent any
  stages {
    stage('Build All Core') {
      steps {
        sh '''cd src
bash build_all_core.sh'''
      }
    }
  }
}