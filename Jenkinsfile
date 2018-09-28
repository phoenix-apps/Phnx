pipeline {
  agent any
  stages {
    stage('Navigate to src') {
      steps {
        sh 'cd src'
      }
    }
    stage('Build All') {
      steps {
        sh 'bash build_all.sh'
      }
    }
  }
}