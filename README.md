## Unity 중상급 기술 Addressable
## 유니티 어드레서블
#### 본 문서는 유튜버 더블엘 DoubleL님의 영상을 참고하여 만들었습니다.

## 개발 환경
Windows Unity C# AWS Visual Studio

## 어드레서블을 쓰는 이유
#### 1. 에셋 빌드와 배포의 단순화 : 직접 레퍼런스, 리소스 폴더, 에셋 번들 분리가 단순화되고 편리해진다.
#### 2. 효율적인 에셋 관리 : 종속성 파악 및 메모리 로드/언로드 현황을 볼 수 있다. (중복된 에셋으로 메모리를 낭비가 되는 것을 막는다.
## 어드레서블 예시
![0](https://github.com/kimkimsun/StudyStorage1/assets/116052108/78bdb2ef-b06d-45a8-9252-a2e82de9066c)

#### 어드레서블을 사용하면 위와 같이 게임의 내용이 달라졌을 때 사용자의 기기에서 그 정보를 알고 다운받을 수 있습니다.
## 사용 법
#### 기본적인 Addressable 다운로드 및 앞으로 있을 서버와의 연동을 위한 AWS 설정 등은 내용에서 생략되었습니다.
#### 혹여나, 내용을 원하시는 분들은 밑에 링크를 참고 하시면 되겠습니다.
https://www.youtube.com/watch?v=uTSxPPaW2-k

## Local Addressable
![1](https://github.com/kimkimsun/StudyStorage1/assets/116052108/aa1efc90-4cfc-437e-b223-5f255d05ec59)
#### Addressable이 제대로 작동하는지 알아보기 위해 기본 무료 에셋들을 배치하였습니다.
![2](https://github.com/kimkimsun/StudyStorage1/assets/116052108/e8d4b7bf-d880-4be2-b6b7-a086dd470dd5)
#### Addressable을 사용하려면 필요한 세팅들에 대해 알려드리겠습니다. 화살표를 클릭해주세요.
![3](https://github.com/kimkimsun/StudyStorage1/assets/116052108/6e424cf7-b259-4942-ad5a-e383b852882a)
![4](https://github.com/kimkimsun/StudyStorage1/assets/116052108/77ffbc83-45ba-4102-bd8b-bc5681319a09)
#### 이것들을 변경하지 않게 된다면 용량적인 문제나 어드레서블을 사용하는 효율성이 매우 떨어지기 때문에 변경해주시길 바랍니다.
#### 4번째 사진에 나와있는 Addressable Setting의 경우 인스펙터에서 세팅에 들어갈 수 없을 수도 있습니다. 그럴 때에는 project탭에서 setting이라고 검색하면 접근 가능합니다.
![5](https://github.com/kimkimsun/StudyStorage1/assets/116052108/ff2e7b07-f0a2-4ba1-98e3-44be7e0654b0)
![6](https://github.com/kimkimsun/StudyStorage1/assets/116052108/bb47e6f1-c82f-4d45-91ad-ad50ed88c226)
![7](https://github.com/kimkimsun/StudyStorage1/assets/116052108/fa57f708-b1ba-46a7-a368-22854e042f8c)
#### 모든 준비를 마쳤으면 에셋들을 프리팹하고, Addressable을 체크를 하면 됩니다.
![8](https://github.com/kimkimsun/StudyStorage1/assets/116052108/1c873eb8-ed69-46c7-98a6-0c18480afa5c)
#### 에셋의 Addressable을 체크를 해도 해당 에셋이 가지고 있는 Material은 Addressable 체크가 안돼있습니다.
#### 이 점 유의하시고 까먹지 말고 Material도 체크 합니다.
![9](https://github.com/kimkimsun/StudyStorage1/assets/116052108/d113c24b-e2a6-4954-b3b0-c85f97dc9c00)
![10](https://github.com/kimkimsun/StudyStorage1/assets/116052108/1f2efb19-277c-49ea-8b81-ff07157a4ec8)
#### Etc라는 어드레서블 그룹을 새로 만들고 구분지어 줄 수도 있습니다.
#### 협업의 과정에서는 해당 과정이 필요하나 혼자 공부하는 용도로 쓰거나 구분이 가능하다면 안해도 전혀 문제 없습니다.
#### 하지만 결국에 개발이라는 것은 나중에라도 협업할 경우를 생각해서 습관을 들이는게 좋습니다.
![11](https://github.com/kimkimsun/StudyStorage1/assets/116052108/b5803563-3a1a-4113-aa2f-b99fa7eac946)
![12](https://github.com/kimkimsun/StudyStorage1/assets/116052108/feb26b94-84aa-4dfa-8f82-847579b66be3)
#### 이름이 너무 길면 해당 객체들을 우클릭해서 simplify 어쩌구를 클릭하시면 됩니다.
![13](https://github.com/kimkimsun/StudyStorage1/assets/116052108/67ace058-4627-4fa6-82c9-6c00f1b4f0d1)
#### 새로 그룹을 만들게 되면 그 그룹도 Remote로 바꾸는 것 또한 잊지 말고 해줍니다.
![14](https://github.com/kimkimsun/StudyStorage1/assets/116052108/9737cd90-f1c9-4b94-844b-ecca93b3ca08)
#### 그럼 기본적인 세팅은 다 마쳤고, AddressableManager라는 오브젝트와 스크립트를 만듭니다.
![15](https://github.com/kimkimsun/StudyStorage1/assets/116052108/7390b283-657d-4194-8876-9f67c179696f)
#### 초기화 해주는 Init 코루틴을 Start에서 제어하게 만듭니다. (해당 부분은 무조건 필요한 부분은 아니지만 혹시 모를 상황에 대비하는 과정입니다.)
![16](https://github.com/kimkimsun/StudyStorage1/assets/116052108/21eba7db-95d5-4d73-b418-a7a9a652bc4e)
#### 그리고 G키를 눌렀을 때 리스트에 추가가 되도록 람다식으로 구현해줍니다.
#### !주의 G키를 계속 누르면 계속 불러서 Unity가 터질 수 있으니 최초실행에 한 번만 되게끔 bool타입으로 제어해주는 것이 좋습니다.
![17](https://github.com/kimkimsun/StudyStorage1/assets/116052108/a15bf4f5-8142-428d-8f08-b6f7047d17e2)
#### 그리고 프리팹 해놓은 에셋들을 지워줍니다.
![18](https://github.com/kimkimsun/StudyStorage1/assets/116052108/702b6c59-28a6-4796-a168-1c40bfb9c444)
![19](https://github.com/kimkimsun/StudyStorage1/assets/116052108/21af1fc6-eef7-4ef7-ae74-3306227754fa)
#### 위와 같이 설정해주시고, G키를 눌렀을 때 잘 불러오는지 확인합니다.
#### 이를 통해 기본적인 Local을 이용한 Addressable세팅 및 처리 과정을 알게 됐습니다, 이제는 AWS와 연동하여 Server에서 관리해보겠습니다.
## Server Addressable
#### 앞서 말씀드렸듯이 AWS계정 생성 및 버킷 생성, 프로젝트와 AWS의 연동 등은 과정에 담기엔 너무 양이 방대하여 생략하였습니다.
![111](https://github.com/kimkimsun/StudyStorage1/assets/116052108/9ce9889b-4121-46fd-a9c9-bd7aedcc7e6c)
![222](https://github.com/kimkimsun/StudyStorage1/assets/116052108/c8e68ff1-9610-4aa4-8e99-a862928d4e15)
![333](https://github.com/kimkimsun/StudyStorage1/assets/116052108/8cf7e8b2-ba87-44c0-931a-1a0108db9091)
#### 기본적인 세팅을 위해 Scene을 3개 만들어 주었습니다.
#### 1. LobbyScene , 2. DownLoadScene, 3. LoadingScene
#### 그러면 기본적으로 해당 씬들이 해내야 될 업무들을 생각해 보겠습니다.
## Lobby
#### 궁극적인 목표 : 로비에서는 버튼을 클릭해 MainScene으로 이동하여야 한다.
#### 1. 만약 패치내용이 있다면 MainScene을 가기 전 DownLoadScene에서 리소스 파일을 다운로드 받은 뒤 LoadingScene을 거쳐 MainScene에 도달해야 한다.
#### 2. 만약 패치내용이 없다면 LoadingScene만 거쳐 MainScene에 도달해야 한다.
## DownLoadScene
#### 궁극적인 목표 : 다운씬에서는 리소스 파일의 크기와 설치가 이루어져야 한다.
#### 1. 리소스 파일의 크기를 제공해주어야 한다.
#### 2. 어떠한 동작을 할 시, 다운로드가 이루어져야 하고, LoadingScene으로 이동해야 한다.
## LoadingScene
#### 궁극적인 목표 : 로딩 뒤, MainScene에 도달하여야한다.
## 본문
#### 그러면 먼저 LobbyScene에 LobbyManager와 스크립트를 생성해 줍니다.
#### LobbyScene에게 가장 중요한 역할은 패치내용이 있고 없고의 차이를 인지해야된다는 것입니다.
![444](https://github.com/kimkimsun/StudyStorage1/assets/116052108/d5b741e6-b2cb-4976-8de5-e5c290f6bb90)
#### 먼저 LobbyManager Start에서 patchSize를 체크합니다.
#### patchSize가 0이 아니라면 다운로드 Scene으로 가고 아니라면 Loading Scene으로 가는 코드입니다.
#### 다음으로 DownLoadScene 속 DownManager 스크립트 입니다.
![555](https://github.com/kimkimsun/StudyStorage1/assets/116052108/f10b8fa6-3ae0-4118-b5a6-e3764fb4f07d)
#### 해당 코드는 patchSize가 있다는 전제하에 진행되어 있는 코드와 함수들이고,
#### 그것에 대한 다운로드를 다 끝낸 뒤, LoadingScene으로 이동하는 코드입니다.
#### 다음은 LoadingScene 속 LoadManager 스크립트 입니다.
![666](https://github.com/kimkimsun/StudyStorage1/assets/116052108/40f34400-8fef-421b-bf97-4b279f492b78)
#### 이 코드는 어느곳에서든 쉽게 볼 수 있는 씬 전환을 해주는 코드입니다.
#### 끝까지 읽어주셔서 감사합니다 코드의 자세한 사항은 깃 확인 부탁드립니다.
## 오류
#### 현재 Addressable이 패치가 되어, 처음 리소스를 실행한 뒤, 실행할 경우 MainScene으로 넘어가지 못하는 상황이 발생한다고 합니다.
#### 이 오류는 외국에서도 발생하고 있으며, 해결방안이 아직 나오지 않은 상태입니다.
#### 당황하지 마시고 다운로드 한 뒤 껐다 키면 정상 실행되는 것을 확인할 수 있습니다.
