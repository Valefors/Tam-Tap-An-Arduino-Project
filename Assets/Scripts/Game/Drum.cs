using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public Enums.TYPE_NOTE drumState = Enums.TYPE_NOTE.NONE;
	Queue<Note> _currentNotes = new Queue<Note>();

    [SerializeField] Note _notePrefab = null;

    private void Start()
    {
        EventsManager.Instance.AddListener<OnTap>(CreateNote);
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
