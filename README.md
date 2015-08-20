# Congress Report Web
'[의원님을 부탁해](https://github.com/codeforseoul/congress-report)' 프로젝트는 지역구 국회의원의 활동 기록을 정리하여 정기적으로 시민들에게 메일을 보내드리는 서비스입니다. 위 소스코드는 사용자가 원하는 지역구 의원의 메일을 받아보기 위한 웹페이지 소스코드입니다.

## 개발 환경

- python 3.4.3
- flask

## 데이터

- [teampopong/data-assembly](https://github.com/teampopong/data-assembly): Korean National Assembly members' data 대한민국 국회위원 아이디, 선거구 등 정보
- election-data.json: 행정구역과 선거구 매칭
- [중앙선거관리위원회 제19대 국회의원 선거 선거구 및 읍면동현황](http://info.nec.go.kr/electioninfo/electionInfo_report.xhtml?electionId=0020120411&requestURI=%2Felectioninfo%2F0020120411%2Fbi%2Fbigi05.jsp&topMenuId=BI&secondMenuId=BIGI&menuId=BIGI05&statementId=BIGI05&electionCode=2&cityCode=1100&townCode=-1&x=18&y=4)

## 개발

<!--
#### 1. bower 라이브러리 설치
```
npm install -g bower
bower install
```
 -->

#### 1. flask 실행
```
pip install flask
python index.py
```

#### 2. 웹페이지 접속
```
http://localhost:8080
```

## 기여자
