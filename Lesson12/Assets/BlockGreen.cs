using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BlockGreen : MonoBehaviour {
	
	public GameObject brokenBlocksPrefab;
	public float hardness = 5f;
	public float stopDetectMagnitude = 0.1f;
	public float stopDetectTime = 1f;
	
	private GameObject gameController;
	private bool isStopChecking  = false;
	private float stopTime;
	
	void Start() {
		// シーンからGameControllerオブジェクトを取得する
		gameController = GameObject.Find("GameController");
	}
		
	void Update () {
		// ブロックの移動速度がしきい値以下かチェック
		if (rigidbody.velocity.magnitude < stopDetectMagnitude) {
			if (!isStopChecking) {
				isStopChecking = true;
				// ブロックが停止した時間を記録
				stopTime = Time.time;
			}
		} else {
			isStopChecking = false;
		}
		// 一定時間停止状態で、Floorの上で停止しているかチェック
		if (isStopChecking 
			&& (Time.time - stopTime) > stopDetectTime 
			&& IsGround()) {
			// GameControllerに成功を伝える
			gameController.SendMessage("StageClear");
		}
	}
	
	// ブロックがFloorの上に接触しているかどうか
	bool IsGround() {
		// Floorが属しているFloorレイヤーのレイヤーマスク
		int layerMaskFloor = 1 << 8;
		// ブロックの下方向にレイを発射してヒットするかチェック
		if (Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y, layerMaskFloor)) {
			return true;
		}
		return false;
	}
	
	// 他のColliderとぶつかった瞬間に呼び出される
	void OnCollisionEnter(Collision collisionInfo) {
		// ぶつかった相手の速度が硬さを上回るかチェック
		if (collisionInfo.relativeVelocity.magnitude > hardness) {
			// ブロック破壊エフェクト用オブジェクトをインスタンス化
			Instantiate(brokenBlocksPrefab, transform.position , brokenBlocksPrefab.transform.rotation);
			// オブジェクトを削除
			Destroy(gameObject);
			// GameControllerに失敗を伝える
			gameController.SendMessage("StageFailed");
		}
    }
}
