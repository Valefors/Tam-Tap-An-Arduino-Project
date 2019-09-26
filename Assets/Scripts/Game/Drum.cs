using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public Enums.TYPE_NOTE drumState = Enums.TYPE_NOTE.NONE;
    Note _currentNote = null;

    [SerializeField] Note _notePrefab = null;

    private void Start()
    {
        EventsManager.Instance.AddListener<OnTap>(CreateNote);
    }

    private void OnTriggerEnter(Collider pCollider)
    {
        if (GameManager.instance.state == Enums.GAME_STATE.CREATE) return;

        if (!pCollider.GetComponent<Note>()) return;

        _currentNote = pCollider.GetComponent<Note>();
        drumState = _currentNote.type;
    }

    void CreateNote(OnTap e)
    {
        if (GameManager.instance.state != Enums.GAME_STATE.CREATE) return;

        Instantiate(_notePrefab, transform.position, Quaternion.identity);
    }

    public void DestroyNote()
    {
        if (_currentNote == null) return;

        if (_currentNote.isLastNote) StartCoroutine(EndCoroutine());

        Destroy(_currentNote.gameObject);
        _currentNote = null;
        drumState = Enums.TYPE_NOTE.NONE;
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
