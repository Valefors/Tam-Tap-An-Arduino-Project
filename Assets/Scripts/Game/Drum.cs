using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public Enums.TYPE_NOTE drumState = Enums.TYPE_NOTE.NONE;
	Queue<Note> _currentNotes = new Queue<Note>();
	float scaleTimer = 0;
	Vector3 scaleNoteLocalScale;

    [SerializeField] Note _notePrefab = null;

    private void Start()
    {
        EventsManager.Instance.AddListener<OnTap>(CreateNote);
    }

	public void SetDifficultyLevel(bool hardMode) {
		GetComponent<SpriteRenderer> ().enabled = hardMode;
		GetComponent<BoxCollider> ().enabled = !hardMode;
		GetComponent<SphereCollider> ().enabled = hardMode;
	}

	private void Update () {
		if (scaleTimer > 0) {
			scaleTimer -= Time.deltaTime;
			if (scaleTimer < 0) {
				scaleTimer = 0;
			}
			if (_currentNotes.Count != 0 && _currentNotes.Peek().type == Enums.TYPE_NOTE.ALL) {
				_currentNotes.Peek().transform.localScale = scaleNoteLocalScale * (1 + 0.2f * Mathf.Sin((0.05f - scaleTimer) * 20 * Mathf.PI));
			}
		}
	}

	private void OnTriggerEnter(Collider pCollider)
    {
        if (GameManager.instance.state == Enums.GAME_STATE.CREATE) return;

        if (!pCollider.GetComponent<Note>()) return;

		_currentNotes.Enqueue (pCollider.GetComponent<Note> ());
		if (_currentNotes.Count == 1) {
			drumState = _currentNotes.Peek().type;
		}
    }

    void CreateNote(OnTap e)
    {
        if (GameManager.instance.state != Enums.GAME_STATE.CREATE) return;

        Instantiate(_notePrefab, transform.position, Quaternion.identity);
    }

	public void ScaleNote() {
		if (_currentNotes.Count == 0 || scaleTimer > 0) return;

		scaleTimer = 0.05f;
		scaleNoteLocalScale = _currentNotes.Peek ().transform.localScale;
	}

    public void DestroyNote()
    {
        if (_currentNotes.Count == 0) return;

        if (_currentNotes.Peek().isLastNote) StartCoroutine(EndCoroutine());

		Note lNote = _currentNotes.Dequeue ();
        Destroy(lNote.gameObject);

		if (_currentNotes.Count == 0) {
			drumState = Enums.TYPE_NOTE.NONE;
		} else {
			drumState = _currentNotes.Peek().type;
		}
    }

	public Note GetLastNote() {
		return _currentNotes.Peek();
	}

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.instance.state == Enums.GAME_STATE.CREATE) return;
        DestroyNote();
    }

    IEnumerator EndCoroutine()
    {
       yield return new WaitForSeconds(2);
       EventsManager.Instance.Raise(new OnEndGame());
    }

    private void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnTap>(CreateNote);
    }
}
