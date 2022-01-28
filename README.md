# Ice Taming

## ChoiSenn
죽을맛

## 0.1.0 (2022.01.01 ~ )


 - [x] 2022.01.02 캐릭터 이동 구현
 - [x] 2022.01.03 화면 설정
 
 - [x] 2022.01.12 애니메이션 및 저속 이동
 - [x] 2022.01.12 배경 스크롤 적용 
 
 - [x] 2022.01.17 캐릭터 샷
 - [x] 2022.01.18 ~ 2022.01.19 적 배치
 - [x] 2022.01.20 피탄 처리
 - [x] 2022.01.20 화면 밖 탄 제거
 - [x] 2022.01.22 플레이어 잔기
 
 - [x] 2022.01.24 플레이어 점수
 - [x] 2022.01.25 인트로 씬
 - [x] 2022.01.25 게임오버 씬
 - [x] 2022.01.26 씬 전환
 - [x] 2022.01.27 ~ 2022.01.28 애니메이션 및 이펙트
 - [x] 2022.01.28 효과음 적용
 - [ ] bgm 적용


# 메모

CrossFadeAlpha 함수로 텍스트 Fade 구현
적 이동경로는 iTween 에셋의 MoveTo 함수를 활용하여 이동경로 미리 설정.
적들은 'Enemy'라는 Tag로 묶여 플레이어 샷과 OnTriggerEnter2D 함수 내에서 Tag 값을 통해 충돌 판정 처리.
충돌하면 적은 비활성화 되고, 충돌 위치의 Rigidbody2D가 추가되어있는 아이템 Prefab을 활성화시킴.
대화 및 스테이지 신은 UGUI Text.
Bomb을 사용하면 적 샷에 대해 무적 판정, 스펠 공격에 Collider를 달아 OnTriggerStay2D를 통해 충돌 판정 실시.
적과 아이템은 오브젝트 풀링을 통해 미리 생성해둔 뒤, 필요할 때 활성화/비활성화 해 재활용하는 방식으로 최적화.
부채꼴 형태 탄막 : N-way
보스가 등장하거나 패턴 변경 시 화면 상의 샷들 삭제. Delegate와 Event 함수를 이용해 삭제하여 최적화.
상태값 변화, 샷 패턴, 적 등장 패턴에 Coroutine 활용.
