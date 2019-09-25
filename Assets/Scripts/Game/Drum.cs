﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public Enums.TYPE_NOTE drumState = Enums.TYPE_NOTE.NONE;
    Note _currentNote = null;

    private void OnTriggerEnter(Collider pCollider)
    {
        if (!pCollider.GetComponent<Note>()) return;

        _currentNote = pCollider.GetComponent<Note>();
        drumState = _currentNote.type;
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
        DestroyNote();
    }

    IEnumerator EndCoroutine()
    {
       yield return new WaitForSeconds(2);
       EventsManager.Instance.Raise(new OnEndGame());
    }
}
