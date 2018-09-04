# DEPRECATED - see developers.blockapps.net for any BlockApps STRATO dev info

# BlockApps: Xamarin Sample App

The Xamarin Sample App is a simple cross-platform todo/task application that
creates todos/tasks as Smart Contracts on a public or private Ethereum
Blockchain.

It supports iOS and Android; the UI is written in native C#. The common code
between the applications resides in a Portable Class Library(PCL). Tasks are
stored as Smart Contracts that hold ether as a reward for each task. Users can
complete a task and earn ether or create a task and spend ether.

The PCL references the [BlockAppsSDK](https://github.com/blockapps/xamarin-sdk)
that allows users to easily interact with an Ethereum Blockchain and the
Contracts on it.

This project type requires Xamarin 3 (Xamarin Studio 5.x) or Visual Studio 2013
with PCL support.

## Prerequisites
* [Bloc*](https://github.com/blockapps/bloc)

<sub>* The sample app is using a remote bloc server (http://40.118.255.235:8000) and strato instance (http://40.118.255.235/eth/v1.2)  for interacting with the instance of the blockchain. You can run your own local bloc server that points to http://strato-dev4.blockapps.net/eth/v1.2 or your own strato instance. </sub>

## Getting Started

Below is a link to a demo video of building the Xamarin Sample App to Android
using Visual Studio 2015

<p align="center">
<a href="https://www.youtube.com/watch?v=xEMApEug2do">
<img border="0" alt="W3Schools" src="https://img.youtube.com/vi/xEMApEug2do/0.jpg" width="50%" height="30%">
</a>
</p>

## Acknowledgements
The Xamarin Sample App is a modified version of
[Tasky](https://github.com/xamarin/mobile-samples/tree/master/TaskyPortable).
Visit their repository to see the original source code.

## License
Copyright [2016] [BlockApps]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
